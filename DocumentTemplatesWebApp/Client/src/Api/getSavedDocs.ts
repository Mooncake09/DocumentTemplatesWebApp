import axios from 'axios';

type Response = string[];

export const getSavedDocs = async (): Promise<Response> => 
{
    try {
        const response = await axios.get<Response>("api/doc/savedFiles");
        console.log({data: response.data});
        
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

export default getSavedDocs;