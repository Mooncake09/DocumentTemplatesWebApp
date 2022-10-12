export type TemplateField = {
    title: string,
    type: string,
    pattern: string
}

export type Template = {
    template: string,
    title: string,
    templateFields: TemplateField[]
}

export default Template;