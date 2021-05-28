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
        script {
          try {
            // check tool installation locations
            powershell """
              msbuild --version
              pumascan --version
            """
          }
          catch(Exception e) {
            powershell "echo 'Init failure.'"
            currentBuild.currentResult = "FAILURE"
          }
          finally {
          }
        }
      }
    }
    stage('Build') {
      steps {
        script {
          try {
            powershell """
              echo "TODO: RESTORE AND BUILD"
            """
          }
          catch(Exception e) {
            powershell "echo 'Build failure.'"
            currentBuild.currentResult = "FAILURE"
          }
          finally {
          }
        }
      }
    }
    stage('Test') {
      steps {
        script {
          try {
            powershell """
              echo "TODO: RUN PUMA"
            """
          }
          catch(Exception ex) {
            powershell "echo 'Test failure.'"
            currentBuild.currentResult = "FAILURE"
          }
          finally {
          }
        }
      }
    }
    stage('Deploy') {
      when {
        beforeAgent true
        not { equals expected: "FAILURE", actual: currentBuild.currentResult }
      }
      steps {
        script {
          try {
            powershell """
              echo "Build and deploy container"
            """
          }
          catch(Exception e) {
            powershell "echo 'Deploy failure.'"
            currentBuild.currentResult = "FAILURE"
          }
          finally {
          }
        }
      }
    }
  }
  post {
    cleanup {
      cleanWs()
    }
  }
}
