import axios from 'axios';
import Template from "../Types/Template"

type GetTemplateResponse = {
    data: Template[]
}

export const getTemplates = async (): Promise<Template[] | undefined> => 
{
    try {
       const { data } = await axios.get<GetTemplateResponse>("api/doc/templates", {
        headers: {
            Accept: 'application/json'
        }
       });

       return data.data;
    } 
    catch(error) {
        if (axios.isAxiosError(error)) {
            console.error('error message: ', error.message);
        } 
        else {
            console.error('unexpected error: ', error);
        }
    }
}

export default getTemplates;