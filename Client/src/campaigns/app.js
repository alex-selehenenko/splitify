const campaignsAddress = 'https://localhost:5001/api/v1/campaigns'

async function fetchAsync(path){
    let response = await fetch(path)
    
    if (response.status === 403){
        window.location.href = "../auth/index.html";
        return undefined;
    }

    return response;
}