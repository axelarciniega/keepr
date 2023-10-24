export class Profile {
  constructor(data) {
    this.profileId = data.profileId
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
    this.coverImage = data.coverImage 
    // TODO add additional properties if needed
  }
}

export class Account extends Profile{
  constructor(data){
    super(data)
    this.email = data.email
  }
}
