pipeline {
  options {
    buildDiscarder(logRotator(numToKeepStr: '15'))
    disableConcurrentBuilds()
    timestamps()
  }
  agent { label 'master' }
  environment {
    SOLUTION = "PumaPrey.sln"

  }
  stages {
    stage('Initialization') {
      steps {
        // check tool installation locations
        powershell """
          msbuild --version
        """
      }
    }
    stage('Build') {
      steps {
        powershell """
          echo "TODO: RESTORE AND BUILD"
        """
      }
    }
    stage('Test') {
      steps {
        powershell """
          echo "TODO: RUN PUMA"
        """
      }
    }
    stage('Deploy') {
      steps {
        powershell """
          echo "Build and deploy container"
        """
      }
    }
  }
  post {
    cleanup {
      cleanWs()
    }
  }
}
