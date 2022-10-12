import React, { FC } from "react";
import type { MenuProps } from "antd";
import { Layout, Menu } from "antd";
const { Header } = Layout;

type Props = {
  renderComponent: Function
}

export const Navbar: FC<Props> = (props: Props) => {
  const { renderComponent } = props;

  // const navbarItems: MenuProps['items'] = ['Новый документ', 'Сохраненные документы'].map(key => ({
  //     key,
  //     label: `${key}`
  // }));
  return (
  <Header className="header">
      <div className="logo" />
      <Menu theme="dark" mode="horizontal" defaultSelectedKeys={['1']} >
        <Menu.Item onClick={() => renderComponent(false)}>
          Новый документ
        </Menu.Item>
        <Menu.Item onClick={() => renderComponent(true)}>
          Сохраненные документы
        </Menu.Item>
      </Menu>
    </Header>
  );
}