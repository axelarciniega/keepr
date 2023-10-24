import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /**@type {Keep[]} */
  keeps: [],

  /**@type {Vault[]} */
  vaults: [],

  /**@type {Vault} */
  activeVault: null,

  /**@type {Keep} */
  activeKeep: null,

  profile: [],
  
  /**@type {Profile} */
  activeProfile: null
})
