import React, { FC, useEffect, useState } from 'react';
import { Space, Card } from 'antd';
import Template from '../../Types/Template';

type Props = {
    template: Template | undefined
}

export const DocumentForm: FC<Props> = (props: Props) => {
    
    return (
        <Space direction="horizontal" size="middle" style={{display: 'flex'}}>
            {props.template?.title}
        </Space>
    );
}

export default DocumentForm;