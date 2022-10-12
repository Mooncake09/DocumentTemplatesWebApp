import axios from 'axios';
import DocumentInputData from '../Types/DocumentInputData';

type Response = Blob | MediaSource;

export const postGenerateDocument = async (data: DocumentInputData): Promise<void> => 
{
    try {
        
        const response = await axios.post<Response>(
            "api/doc/word",
             JSON.stringify(data),
             {
                headers: {
                    'Content-Type': 'application/json',
                    Accept: 'application/json',
                },
                responseType: 'blob'
             }
        );
        
        const href = URL.createObjectURL(response.data);

        // create "a" HTML element with href to file & click
        const link = document.createElement('a');
        link.href = href;
        link.setAttribute('download', `${data.template}`); //or any other extension
        document.body.appendChild(link);
        link.click();
    
        // clean up "a" element & remove ObjectURL
        document.body.removeChild(link);
        URL.revokeObjectURL(href);
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