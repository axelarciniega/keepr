import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService{

    async createVault(vaultData){
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.unshift(new Vault(res.data))
    }

    async vaultDetails(vaultId){
        const res = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = new Vault(res.data)
    }

    async getMyVaults(profileId){
        const res = await api.get(`api/accounts/vaults/${profileId}`)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

    async vaultProfile(profileId){
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

    async removeVault(vaultId){
        const res = await api.delete(`api/vaults/${vaultId}`)
        logger.log(res.data, 'deleting vault')
        const findIndex = AppState.vaults.findIndex(v => v.id == vaultId)
        AppState.vaults.splice(findIndex, 1)
        AppState.activeVault = {}
    }

}

export const vaultsService = new VaultsService()