pipeline {
    agent any

    environment {
        GIT_REPO = 'https://github.com/duy1707gg/student-management-dotnet.git'
        IMAGE_NAME = 'duytranhn1707/student-management-dotnet'
        TAG = 'latest'
        ARTIFACT_PATH = "${env.WORKSPACE}\\artifacts"
        IIS_DEPLOY_PATH = 'C:\\inetpub\\wwwroot\\student-management'
        SOLUTION = "${env.WORKSPACE}\\student-management-dotnet.sln"
        CSPROJ = "${env.WORKSPACE}\\student-management-dotnet.csproj"
    }

    stages {
        stage('Checkout') {
            steps {
                echo '📥 Checking out code from GitHub...'
                git branch: 'master', url: "${env.GIT_REPO}"
            }
        }

        stage('Clean Artifacts') {
            steps {
                echo '🧹 Cleaning old artifacts...'
                bat "rmdir /S /Q \"${env.ARTIFACT_PATH}\" || exit 0"
                bat "mkdir \"${env.ARTIFACT_PATH}\""
            }
        }

        stage('Restore') {
            steps {
                echo '🔧 Restoring NuGet packages...'
                bat "dotnet restore \"${env.SOLUTION}\""
            }
        }

        stage('Build') {
            steps {
                echo '⚙️ Building solution...'
                bat "dotnet build \"${env.SOLUTION}\" --configuration Release --no-restore"
            }
        }

        stage('Publish') {
            steps {
                echo '📦 Publishing project...'
                bat "dotnet publish \"${env.CSPROJ}\" -c Release -o \"${env.ARTIFACT_PATH}\" /p:PublishSingleFile=false /p:GenerateRuntimeConfigurationFiles=true"
            }
        }

        stage('Build Docker Image') {
            steps {
                echo '🐳 Building Docker image...'
                script {
                    docker.build("${IMAGE_NAME}:${TAG}", '.')
                }
            }
        }

        stage('Push Docker Image') {
            steps {
                echo '📤 Pushing Docker image to Docker Hub...'
                withCredentials([usernamePassword(credentialsId: 'dockerhub-credentials', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    script {
                        docker.withRegistry('https://index.docker.io/v1/', 'dockerhub-credentials') {
                            docker.image("${IMAGE_NAME}:${TAG}").push()
                        }
                    }
                }
            }
        }

        stage('Deploy to IIS - Copy') {
            steps {
                echo '📁 Copying published files to IIS folder...'
                bat "if not exist \"${env.IIS_DEPLOY_PATH}\" mkdir \"${env.IIS_DEPLOY_PATH}\""
                bat "xcopy /E /Y /I /R \"${env.ARTIFACT_PATH}\\*\" \"${env.IIS_DEPLOY_PATH}\""
            }
        }

        stage('List deployed files') {
            steps {
                echo '📝 Listing deployed files...'
                bat "dir \"${env.IIS_DEPLOY_PATH}\" /s"
            }
        }

        stage('Deploy to IIS - Website Setup') {
            steps {
                echo '🌐 Configuring IIS Website...'
                powershell '''
                    Import-Module WebAdministration
                    $siteName = "StudentManagement"
                    $sitePath = "C:\\inetpub\\wwwroot\\student-management"
                    $port = 8090

                    if (-not (Test-Path "IIS:\\Sites\\$siteName")) {
                        New-Website -Name $siteName -Port $port -PhysicalPath $sitePath -ApplicationPool "DefaultAppPool"
                        Write-Host "✅ Website '$siteName' created on port $port."
                    } else {
                        Write-Host "ℹ️ Website '$siteName' already exists."
                    }
                '''
            }
        }
    }

    post {
        success {
            echo "✅ Deployment completed successfully!"
        }
        failure {
            echo "❌ Deployment failed. Check the logs for details."
        }
    }
}
