export type DocumentInputData = {
    template?: string;
    content?: {[id: string]: string | number}
}

type InputData = {
    pattern: string;
    value: string | number;
}

export default DocumentInputData;