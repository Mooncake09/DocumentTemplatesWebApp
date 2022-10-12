import { List } from "antd";
import React, { FC, useEffect, useState } from "react";
import getSavedDocs from "../../Api/getSavedDocs";
import getSavedFile from "../../Api/getSavedFile";

export const SavedDocsList: FC = () => {
    const [fileList, setFileList] = useState<string[]>([]);

    useEffect(() => {
        getSavedDocs().then(res => setFileList(res));
    }, []);

    return(
        <List
            size="small"
            header={<div>Сохраненные файлы</div>}
            bordered
        >
            {fileList.map(file => 
            <List.Item
                onClick={() => getSavedFile(file)}
            >
                <a>{file}</a>
            </List.Item>)}
        </List>
    );
}