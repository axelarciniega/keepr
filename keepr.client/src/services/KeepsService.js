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


}


export const keepsService = new KeepsService()