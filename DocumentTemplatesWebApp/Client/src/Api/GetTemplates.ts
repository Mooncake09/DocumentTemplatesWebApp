import axios from 'axios';
import Template from "../Types/Template"

type Response = Template[];

export const getTemplates = async (): Promise<Response> => 
{
    try {
        const response = await axios.get<Response>("api/doc/templates");
        return response.data;
    } 
    catch(error) {
        if (axios.isAxiosError(error)) {
            console.error('error message: ', error.message);
            throw(error);
        } 
        else {
            console.error('unexpected error: ', error);
            throw(error);
        }
    }
}

export default getTemplates;