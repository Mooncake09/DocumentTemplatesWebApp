import axios from 'axios';

type Response = Blob | MediaSource;

export const getSavedFile = async (fileName: string): Promise<void> => 
{
    try {
        const response = await axios.get<Response>(
            `api/doc/savedFile/${fileName}`,
            {
            responseType: 'blob'
            }
        );
        
        const href = URL.createObjectURL(response.data);

        // create "a" HTML element with href to file & click
        const link = document.createElement('a');
        link.href = href;
        link.setAttribute('download', fileName); //or any other extension
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

export default getSavedFile;