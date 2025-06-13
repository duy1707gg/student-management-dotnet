pipeline {
    agent any

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
                bat 'dotnet restore student-management-dotnet.sln'
            }
        }

        stage('Build') {
            steps {
                echo '⚙️ Building solution...'
                bat 'dotnet build student-management-dotnet.sln --configuration Release'
            }
        }

        stage('Publish') {
            steps {
                echo '📦 Publishing project...'
                bat 'dotnet publish student-management-dotnet.csproj -c Release -o ./artifacts /p:PublishSingleFile=false'
            }
        }

        stage('Deploy to IIS - Copy') {
            steps {
                echo '📁 Copying to IIS directory...'
                bat 'xcopy "%WORKSPACE%\\artifacts" "C:\\wwwroot\\myproject" /E /Y /I /R'
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
