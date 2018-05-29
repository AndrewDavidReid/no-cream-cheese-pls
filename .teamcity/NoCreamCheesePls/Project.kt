package NoCreamCheesePls

import NoCreamCheesePls.buildTypes.*
import NoCreamCheesePls.vcsRoots.*
import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.Project

object Project : Project({
  uuid = "0381cae3-742c-4d18-95e8-3695e28909d8"
  id = "NoCreamCheesePls"
  parentId = "_Root"
  name = "No Cream Cheese Pls"

  vcsRoot(Vcs_NoCreamCheesePls_NoCreamCheesePls)

  buildType(NoCreamCheesePls_NoCreamCheesePls)
})
