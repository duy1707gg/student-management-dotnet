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
        APP_POOL_NAME = 'DefaultAppPool'
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
                bat "IF EXIST \"${env.ARTIFACT_PATH}\" rmdir /S /Q \"${env.ARTIFACT_PATH}\""
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
                bat "dotnet publish \"${env.CSPROJ}\" -c Release -o \"${env.ARTIFACT_PATH}\""
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

        stage('Stop IIS AppPool') {
            steps {
                echo '⛔ Stopping IIS App Pool...'
                powershell '''
                    Import-Module WebAdministration
                    if (Test-Path "IIS:\\AppPools\\${env:APP_POOL_NAME}") {
                        $state = (Get-WebAppPoolState -Name "${env:APP_POOL_NAME}").Value
                        if ($state -eq "Started") {
                            Stop-WebAppPool -Name "${env:APP_POOL_NAME}"
                            Write-Host "✅ AppPool stopped."
                        } else {
                            Write-Host "ℹ️ AppPool already stopped."
                        }
                    } else {
                        Write-Host "⚠️ AppPool not found: ${env:APP_POOL_NAME}"
                    }
                '''
            }
        }

        stage('Deploy to IIS - Copy') {
            steps {
                echo '📁 Copying published files to IIS folder...'
                bat "if not exist \"${env.IIS_DEPLOY_PATH}\" mkdir \"${env.IIS_DEPLOY_PATH}\""
                bat """
                    robocopy \"${env.ARTIFACT_PATH}\" \"${env.IIS_DEPLOY_PATH}\" /E /Z /NP /NFL /NDL /R:3 /W:5
                    IF %ERRORLEVEL% GEQ 8 (
                        echo ❌ Robocopy failed with error level %ERRORLEVEL%
                        exit /b %ERRORLEVEL%
                    ) else (
                        echo ✅ Robocopy succeeded with error level %ERRORLEVEL%
                        exit /b 0
                    )
                """
            }
        }

        stage('Start IIS AppPool') {
            steps {
                echo '🚀 Starting IIS App Pool...'
                powershell '''
                    Import-Module WebAdministration
                    if (Test-Path "IIS:\\AppPools\\${env:APP_POOL_NAME}") {
                        $state = (Get-WebAppPoolState -Name "${env:APP_POOL_NAME}").Value
                        if ($state -eq "Stopped") {
                            Start-WebAppPool -Name "${env:APP_POOL_NAME}"
                            Write-Host "✅ AppPool started."
                        } else {
                            Write-Host "ℹ️ AppPool already running."
                        }
                    } else {
                        Write-Host "⚠️ AppPool not found: ${env:APP_POOL_NAME}"
                    }
                '''
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
                    $sitePath = "${env:IIS_DEPLOY_PATH}"
                    $port = 8090

                    if (-not (Test-Path "IIS:\\Sites\\$siteName")) {
                        New-Website -Name $siteName -Port $port -PhysicalPath $sitePath -ApplicationPool "${env:APP_POOL_NAME}"
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
