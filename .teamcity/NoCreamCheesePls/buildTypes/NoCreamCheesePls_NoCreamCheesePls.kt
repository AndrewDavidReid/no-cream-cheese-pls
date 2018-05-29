package NoCreamCheesePls.buildTypes

import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.buildSteps.script

object NoCreamCheesePls_NoCreamCheesePls : BuildType({
  uuid = "f1422e40-8117-4259-8f8d-3e98897ae92a"
  id = "NoCreamCheesePls_NoCreamCheesePls"
  name = "Development"

  vcs {
    root(NoCreamCheesePls.vcsRoots.Vcs_NoCreamCheesePls_NoCreamCheesePls)

    cleanCheckout = true
  }

  steps {
    script {
      name = "Say hello"
      scriptContent = """echo "hi"""""
    }
  }
})
