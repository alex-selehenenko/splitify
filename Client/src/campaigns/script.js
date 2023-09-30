const campaignsPath = 'https://localhost:7042/api/v1/campaigns';
const redirectPath = 'https://localhost:5656/'

function formatDateTime(inputDateTime) {
    const dateTime = new Date(inputDateTime);
    
    const day = String(dateTime.getDate()).padStart(2, '0');
    const month = String(dateTime.getMonth() + 1).padStart(2, '0');
    const year = dateTime.getFullYear();
    
    const hours = String(dateTime.getHours()).padStart(2, '0');
    const minutes = String(dateTime.getMinutes()).padStart(2, '0');
    const seconds = String(dateTime.getSeconds()).padStart(2, '0');
    
    return `${day}.${month}.${year} ${hours}:${minutes}:${seconds}`;
}

function getRedirectUrl(id){
    return redirectPath + id;
}

class Application{
    
    constructor(){
        this.campaigns = [];
        this.campaignsLoadedEventType = 'campaignsLoaded'
    }

    run(){
        this.fetchCampaigns()
            .then(data => {
                this.campaigns = data;
                this.dispatchEvent(this.campaignsLoadedEventType);               
            })
            .catch(error => console.log(error));
    }

    async fetchJson(path){
        let response = await fetch(path)
        
        if (response.status === 403){
            window.location.href = "../auth/index.html";
            return undefined;
        }
    
        return await response.json();
    }
    
    async fetchCampaigns(){
        return await this.fetchJson(campaignsPath);
    }

    dispatchEvent(eventType){
        const event = new Event(eventType);
        document.dispatchEvent(event);
    }
}

const app = new Application();
app.run();