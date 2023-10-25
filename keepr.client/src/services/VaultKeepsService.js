import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService{

    async createVaultKeep(formData){
        const res = await api.post('api/vaultKeeps', formData)
        logger.log(res.data, 'created vault keep')
    }

    async removeVaultKeep(vaultKeepId){
        const res = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
        logger.log(res.data, 'deleting vaultKeep')
        const vaultKkeepIndex = AppState.keeps.findIndex(k => k.vaultKeepId == vaultKeepId)
        AppState.keeps.splice(vaultKkeepIndex, 1)
        AppState.activeKeep = {}

    }


}



export const vaultKeepsService = new VaultKeepsService()