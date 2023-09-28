pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(credentialsId: '9f17f773-a128-443e-9e59-950b688a036f', url: 'https://github.com/Adityageek/Car-Store.git', branch: 'master')
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet build C:\\Workspace\\CarStore\\CarStore.sln --configuration Release'
      }
    }

    stage('Release') {
      steps {
        bat 'dotnet build  C:\\Workspace\\CarStore\\CarStore.sln /p:PublishProfile="C:\\Workspace\\CarStore\\Services\\CarSeller\\Properties\\PublishProfiles\\JenkinsProfile.pubxml" /p:Platform="Any CPU" /p:DeployOnBuild=true /m'
      }
    }

    stage('Deploy') {
      steps {
        bat 'net stop "w3svc"'
        bat '"C:\\Program Files (x86)\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:package="C:\\Workspace\\CarStore\\Services\\CarSeller\\bin\\Debug\\net7.0\\CarSeller.zip" -dest:auto -setParam:"IIS Web Application Name"="CarStoreWeb" -skip:objectName=filePath,absolutePath=".\\\\PackageTmp\\\\Web.config$" -enableRule:DoNotDelete -allowUntrusted=true'
        bat 'net start "w3svc"'
      }
    }

  }
  environment {
    dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
  }
}