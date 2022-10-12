export type DocumentInputData = {
    template?: string;
    inputData?: InputData[]
}

type InputData = {
    pattern: string;
    value: string | number;
}

export default DocumentInputData;