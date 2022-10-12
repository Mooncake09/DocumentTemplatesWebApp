import axios from 'axios';
import DocumentInputData from '../Types/DocumentInputData';

type Response = string;

export const postGenerateDocument = async (data: DocumentInputData): Promise<Response> => 
{
    try {
        console.log({data});
        
        const response = await axios.post<Response>(
            "api/doc/word",
             JSON.stringify(data),
             {
                headers: {
                    'Content-Type': 'application/json',
                    Accept: 'application/json',
                  }
             }
             );

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

export default postGenerateDocument;