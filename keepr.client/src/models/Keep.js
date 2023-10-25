

export class Keep{
    constructor(data){
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.creatorId = data.creatorId
        this.kept = data.kept
        this.creator = data.creator
        this.creatorPic = data.creator.picture

        this.vaultKeepId = data.vaultKeepId
    }
}