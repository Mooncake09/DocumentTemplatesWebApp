import axios from 'axios';

type Response = {
    data: string
}

export const getTemplates: any = () => 
{
    try {
       const response: any = axios.get("api/doc/templates").then(res => {
        console.log(res);
       });
       return response;
    } 
    catch(error) {
        console.error(error);
    }
}

export default getTemplates;