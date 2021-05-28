pipeline {
  options {
    buildDiscarder(logRotator(numToKeepStr: '15'))
    disableConcurrentBuilds()
    timestamps()
  }
  agent { label 'master' }
  environment {
    SOLUTION = "./PumaPrey.sln"
    CONFIGURATION = "Release"
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
          nuget restore ${env.SOLUTION}
          msbuild ${env.SOLUTION} /p:Configuration=${env.CONFIGURATION}
        """
      }
    }
    stage('Test') {
      steps {
        powershell """
          pumascan scan -p ${env.SOLUTION} -s ./.pumafile -o ./pumascan -f msbuild,json,html
        """
        recordIssues(tools: [msBuild(pattern: 'pumascan.msbuild')])
        archiveArtifacts "pumascan*"
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
