package NoCreamCheesePls.vcsRoots

import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.vcs.GitVcsRoot

object Vcs_NoCreamCheesePls_NoCreamCheesePls : GitVcsRoot({
  uuid = "a13744f0-0385-4170-9cdb-e5bf48ebbab4"
  id = "NoCreamCheesePls_NoCreamCheesePls"
  name = "no-cream-cheese-pls"
  url = "git@gitlab.com:adr-projects/no-cream-cheese-pls.git"
  branch = "stable"
  branchSpec = "+:refs/heads/*"
  authMethod = uploadedKey {
    uploadedKey = "id_rsa"
  }
})
