import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService{

    async createKeep(keepData){
        const res = await api.post('api/keeps', keepData)
        logger.log('creating keep', res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }

    async getKeeps(){
        const res = await api.get('api/keeps')
        logger.log('getting keeps', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getKeepDetails(keepId){
        const res = await api.get(`api/keeps/${keepId}`)
        logger.log('getting keep by Id', res.data)
        AppState.activeKeep = new Keep(res.data)
    }

    async getKeepsByVaultId(vaultId){
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.keeps = res.data.map(kee => new Keep(kee))
    }

    async getMyKeeps(profileId){
        const res = await api.get(`api/accounts/${profileId}/keeps`)
        AppState.keeps = res.data.map(kee => new Keep(kee))
    }

    async keepProfile(profileId){
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.keeps = res.data.map(kee => new Keep(kee))
    }

    async removeKeep(keepId){
        const res = await api.delete(`api/keeps/${keepId}`)
        logger.log(res.data, 'deleting keep')
        const findIndex = AppState.keeps.findIndex(kee => kee.id == keepId)
        AppState.keeps.splice(findIndex, 1)
        AppState.activeKeep = {}
    }


}


export const keepsService = new KeepsService()