
export default function (callback)
{
    window.fetch("http://localhost:62060/api/ping")
      .then(function(response) {
        return response.json()
      })
      .then(function(body){
        callback(body)
      })
      .catch(function(ex) {
        callback(undefined, ex)
      })
}