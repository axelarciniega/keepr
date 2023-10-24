import { AppState } from "../AppState"

class ClearService{

    async clearAppstate(){
        AppState.activeProfile = {}
        AppState.activeKeep = {}
        AppState.vaults = {}
        AppState.keeps = {}
    }

    clearVaultAppstate(){
        AppState.keeps = {}
        AppState.activeKeep = {}
        AppState.activeVault = {}
    }

}


export const clearService = new ClearService()