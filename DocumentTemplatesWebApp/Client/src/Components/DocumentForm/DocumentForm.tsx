import React, { FC, useEffect, useState } from 'react';
import { Space, Card, Input, Button } from 'antd';
import Template, { TemplateField } from '../../Types/Template';
import DocumentInputData from '../../Types/DocumentInputData';
import postGenerateDocument from '../../Api/postGenerateDocument';

type Props = {
    template: Template | undefined
}

export const DocumentForm: FC<Props> = (props: Props) => {

    const [inputFieldsData, setInputFieldsData] = useState<DocumentInputData>(
    {
        template: props.template?.template,
        content: {}
    });

    const { template } = props;

    console.log({inputFieldsData});

    const getInputFields: any = () => {
        const elements = template?.templateFields.map(field => {
            switch (field.type) {
                case "string":
                    return (
                        <div key={field.pattern} style={{marginTop: "10px"}}>
                            <label>{field.title}</label>
                            <Input placeholder={field.title} type='text' onChange={(event) => onInputDataChange(field, event.target.value)} required/>
                        </div>
                    );
                case "date":
                    return (
                        <div key={field.pattern}>
                            <label>{field.title}</label>
                            <Input placeholder={field.title} type='date' onChange={(event) => onInputDataChange(field, event.target.value)} required/>
                        </div>
                    );
                case "number":
                    return (
                        <div key={field.pattern}>
                            <label>{field.title}</label>
                            <Input placeholder={field.title} type='number' onChange={(event) => onInputDataChange(field, event.target.value)} required/>
                        </div>
                    );
                default:
                    break;
            }
        });
        return elements;
    }

    const onInputDataChange = (templateFieldData: TemplateField, inputValue: string | number): void => {
        const newInputData = inputFieldsData?.content;
        newInputData![templateFieldData.pattern] = inputValue; 

        // const patternElement = newInputData?.find(item => item.pattern === templateFieldData.pattern);

        // if (patternElement) {
        //     patternElement.value = inputValue;
        // }
        // else {
        //     newInputData?.push({
        //         pattern: templateFieldData.pattern,
        //         value: inputValue
        //     });
        // }
        // const dict: {[id: string]: string | number} = {};
        // newInputData.map(item => {
        //     dict[`${item.pattern}`] = item.value;
        // });
        // console.log({dict});
        
        setInputFieldsData({
            template: inputFieldsData.template,
            content: newInputData
        });
    }
    
    return (
        <Space direction="horizontal" size="middle" style={{display: 'flex'}}>
            <Card title={props.template?.title} style={{width: '700px'}}>
                {getInputFields()}
                <Button type="primary" style={{marginTop: '10px'}} onClick={() => postGenerateDocument(inputFieldsData)}>Сгенерировать документ</Button>
            </Card>
        </Space>
    );
}

export default DocumentForm;