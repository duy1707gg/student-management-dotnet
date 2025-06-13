pipeline {
    agent any

    environment {
        DOTNET_ROOT = 'C:\\Program Files\\dotnet'
        PUBLISH_DIR = 'publish'
        IIS_TARGET = 'C:\\inetpub\\student-management'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/your-username/student-management-dotnet.git'
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
                bat 'dotnet publish student-management-dotnet/student-management-dotnet.csproj --configuration Release --output %PUBLISH_DIR%'
            }
        }

        stage('Deploy to IIS') {
            steps {
                bat "xcopy /Y /E /I %PUBLISH_DIR% %IIS_TARGET%"
            }
        }
    }

    post {
        success {
            echo '✅ Deploy thành công!'
        }
        failure {
            echo '❌ Deploy thất bại.'
        }
    }
}
