pipeline {
    agent any

    environment {
        IMAGE_NAME = 'duy1707gg/student-management-dotnet'
        TAG = 'latest'
    }

    stages {
        stage('Checkout') {
            steps {
                echo '📥 Checking out code...'
                git branch: 'master', url: 'https://github.com/duy1707gg/student-management-dotnet.git'
            }
        }

        stage('Restore') {
            steps {
                echo '🔧 Restoring NuGet packages...'
                bat 'dotnet restore D:\\student-management-dotnet\\student-management-dotnet.sln'
            }
        }

        stage('Build') {
            steps {
                echo '⚙️ Building solution...'
                bat 'dotnet build D:\\student-management-dotnet\\student-management-dotnet.sln --configuration Release'
            }
        }

        stage('Publish') {
            steps {
                echo '📦 Publishing project...'
                bat 'dotnet publish D:\\student-management-dotnet\\student-management-dotnet.csproj -c Release -o D:\\student-management-dotnet\\artifacts /p:PublishSingleFile=false'
            }
        }

stage('Build Docker Image') {
    steps {
        echo '🐳 Building Docker image...'
        script {
            dir('D:\\student-management-dotnet') {
                docker.build("${IMAGE_NAME}:${TAG}", ".")
            }
        }
    }
}

        stage('Push Docker Image') {
            steps {
                echo '📤 Pushing Docker image...'
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
                echo '📁 Copying to IIS directory...'
                bat 'xcopy "D:\\student-management-dotnet\\artifacts" "C:\\wwwroot\\myproject" /E /Y /I /R'
            }
        }

        stage('List files') {
            steps {
                echo '📝 Listing deployed files...'
                bat 'dir "C:\\wwwroot\\myproject" /s'
            }
        }

        stage('Deploy to IIS - Website Setup') {
            steps {
                echo '🌐 Configuring IIS Website...'
                powershell '''
                    Import-Module WebAdministration
                    $siteName = "MySite"
                    $sitePath = "C:\\wwwroot\\myproject"
                    $port = 81

                    if (-not (Test-Path "IIS:\\Sites\\$siteName")) {
                        New-Website -Name $siteName -Port $port -PhysicalPath $sitePath -ApplicationPool "DefaultAppPool"
                    } else {
                        Write-Output "Website '$siteName' already exists."
                    }
                '''
            }
        }
    }
}
