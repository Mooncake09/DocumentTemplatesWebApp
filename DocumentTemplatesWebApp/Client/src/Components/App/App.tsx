import React, { FC, useEffect, useState } from 'react';
import { FormOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Layout } from 'antd';
import './App.css';
import getTemplates from '../../Api/getTemplates';
import Template from '../../Types/Template';
import { Navbar } from '../Navbar/Navbar';
import { SiderMenu } from '../SiderMenu/SiderMenu';
import DocumentForm from '../DocumentForm/DocumentForm';

const { Content, Footer } = Layout;

const App: FC = () => {
  const [templates, setTemplates] = useState<Template[]>([]);
  const [selectedTemplate, setSelectedTemplate] = useState<string>();

  useEffect(() => {
    getTemplates().then(res => {
      setTemplates(res);
    }, err => {
      console.error(err);
    });
  }, []);

  const changeTemplate = (template: string) => {
    setSelectedTemplate(template);
  }

  return (
    <Layout>
      <Navbar />
      <Content style={{ padding: '0 50px' }}>
        <Layout className="site-layout-background" style={{ padding: '24px 0' }}>
          <SiderMenu templates={templates?.map(item => item.title)} changeTemplate={changeTemplate} />
          <Content style={{ padding: '0 24px', minHeight: 280 }}>
            {selectedTemplate ? <DocumentForm template={templates.find(item => item.title === selectedTemplate)}/> : null }
          </Content>
        </Layout>
      </Content>
      <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
    </Layout>
  );
}

export default App;
