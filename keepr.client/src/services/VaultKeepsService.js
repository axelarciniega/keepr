import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService{

    async createVaultKeep(formData){
        const res = await api.post('api/vaultKeeps', formData)
        logger.log(res.data, 'created vault keep')
    }



}



export const vaultKeepsService = new VaultKeepsService()