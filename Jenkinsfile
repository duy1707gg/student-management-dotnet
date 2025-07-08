pipeline {
    agent any

    environment {
        GIT_REPO = 'https://github.com/duy1707gg/student-management-dotnet.git'
        IMAGE_NAME = 'duytranhn1707/student-management-dotnet'
        TAG = 'latest'
        ARTIFACT_PATH = 'D:\\publish-temp'
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
                echo '🧹 Cleaning old publish folder...'
                bat "IF EXIST \"${env.ARTIFACT_PATH}\" rmdir /S /Q \"${env.ARTIFACT_PATH}\""
                bat "mkdir \"${env.ARTIFACT_PATH}\""
            }
        }

        stage('Restore') {
            steps {
                echo '🔧 Restoring packages...'
                bat "dotnet restore \"${env.SOLUTION}\""
            }
        }

        stage('Build') {
            steps {
                echo '⚙️ Building project...'
                bat "dotnet build \"${env.SOLUTION}\" -c Release --no-restore"
            }
        }

        stage('Publish') {
            steps {
                echo '📦 Publishing project...'
                bat "dotnet publish \"${env.CSPROJ}\" -c Release -o \"${env.ARTIFACT_PATH}\""
            }
        }

        stage('Stop IIS AppPool') {
            steps {
                echo '🛑 Stopping IIS App Pool...'
                powershell '''
                    Import-Module WebAdministration
                    if (Test-Path "IIS:\\AppPools\\${env:APP_POOL_NAME}") {
                        $state = (Get-WebAppPoolState -Name "${env:APP_POOL_NAME}").Value
                        if ($state -eq "Started") {
                            Stop-WebAppPool -Name "${env:APP_POOL_NAME}"
                            Write-Host "✅ AppPool stopped."
                        }
                    }
                '''
            }
        }

        stage('Deploy to IIS') {
            steps {
                echo '📁 Copying to IIS folder...'
                bat """
                    IF EXIST \"${env.IIS_DEPLOY_PATH}\" (
                        rmdir /S /Q \"${env.IIS_DEPLOY_PATH}\"
                    )
                    mkdir \"${env.IIS_DEPLOY_PATH}\"
                    robocopy \"${env.ARTIFACT_PATH}\" \"${env.IIS_DEPLOY_PATH}\" /E /Z /NP /NFL /NDL /R:3 /W:3
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
                        }
                    }
                '''
            }
        }

        stage('IIS Website Setup') {
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

        stage('List Files') {
            steps {
                bat "dir \"${env.IIS_DEPLOY_PATH}\" /s"
            }
        }
    }

    post {
        success {
            echo "✅ Deployment completed successfully!"
        }
        failure {
            echo "❌ Deployment failed. Check the logs!"
        }
    }
}
