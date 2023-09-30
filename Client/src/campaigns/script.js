const campaignsPath = 'https://localhost:7042/api/v1/campaigns';

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