import api from "./api"

api((body, error) => {

    if(error){
        console.log("Error", error)
        return
    }
        
    console.log("Body", body)
})
