import React, { FC } from "react";
import type { MenuProps } from "antd";
import { Layout, Menu } from "antd";
const { Header } = Layout;

export const Navbar: FC = () => {
    const navbarItems: MenuProps['items'] = ['Новый документ', 'Сохраненные документы'].map(key => ({
        key,
        label: `${key}`
      }));
    return (
    <Header className="header">
        <div className="logo" />
        <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']} items={navbarItems} />
      </Header>
    );
}