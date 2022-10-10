import React, { FC } from 'react';
import { FormOutlined } from '@ant-design/icons';
import type { MenuProps } from 'antd';
import { Breadcrumb, Layout, Menu } from 'antd';
import './App.css';
import getTemplates from '../../Api/getTemplates';

const { Header, Content, Footer, Sider } = Layout;

const navbarItems: MenuProps['items'] = ['Новый документ', 'Сохраненные документы'].map(key => ({
  key,
  label: `${key}`,
}));

const sidebarItems: MenuProps['items'] = [FormOutlined].map(
  (icon, index) => {
    const key = String(index + 1);

    return {
      key: `sub${key}`,
      icon: React.createElement(icon),
      label: `Шаблоны`,

      children: new Array(4).fill(null).map((_, j) => {
        const subKey = index * 4 + j + 1;
        return {
          key: subKey,
          label: `option${subKey}`,
        };
      }),
    };
  },
);

const App: FC = () => (
  <Layout>
    <Header className="header">
      <div className="logo" />
      <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']} items={navbarItems} />
    </Header>
    <Content style={{ padding: '0 50px' }}>
      <Layout className="site-layout-background" style={{ padding: '24px 0' }}>
        <Sider className="site-layout-background" width={200}>
          <Menu
            mode="inline"
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            style={{ height: '100%' }}
            items={sidebarItems}
          />
        </Sider>
        <Content style={{ padding: '0 24px', minHeight: 280 }}>

        </Content>
      </Layout>
    </Content>
    <Footer style={{ textAlign: 'center' }}>Ant Design ©2018 Created by Ant UED</Footer>
</Layout>
);

export default App;
