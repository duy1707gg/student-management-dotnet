pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'master', url: 'https://github.com/duy1707gg/student-management-dotnet.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore student-management-dotnet.sln'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build student-management-dotnet.sln --configuration Release'
            }
        }

stage('Publish') {
    steps {
        bat 'dotnet publish student-management-dotnet.csproj -c Release -o ./artifacts'
    }
}

stage('Deploy to IIS') {
    steps {
        bat 'xcopy "%WORKSPACE%\\artifacts" /E /Y /I /R "c:\\wwwroot\\myproject"'
    }
}

stage('List files') {
    steps {
        bat 'dir /s'
    }
}


        stage('Deploy to IIS1111') {
            steps {
                powershell '''
                    Import-Module WebAdministration
                    if (-not (Test-Path IIS:\\Sites\\MySite)) {
                        New-Website -Name "MySite" -Port 81 -PhysicalPath "c:\\wwwroot\\myproject"
                    }
                '''
            }
        }
    }
}
