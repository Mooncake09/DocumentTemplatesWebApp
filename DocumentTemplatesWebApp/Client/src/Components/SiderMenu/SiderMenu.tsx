import React, { FC, useState } from "react";
import { Layout, Menu } from 'antd';
import type { MenuProps } from "antd";
import { FormOutlined } from '@ant-design/icons';
import { title } from "process";
const { Sider } = Layout;
type MenuItem = Required<MenuProps>['items'][number];

type Props = {
  templates: string[]
}

function getItem(
  label: React.ReactNode,
  key: React.Key,
  icon?: React.ReactNode,
  children?: MenuItem[],
  type?: 'group',
  onClick?: Function
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
    type,
    onClick
  } as MenuItem;
}

export const SiderMenu: FC<Props> = (props: Props) => {

  const { templates } = props;
  const [template, setTemplate] = useState<string>("not chosen");

  const changeTemplate = (template: string) => {
    setTemplate(template);
  }

  console.log(template);
  
  return(
      <Sider className="site-layout-background" width={200}>
          <Menu
            mode="inline"
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            style={{ height: '100%' }}
          >
            <Menu.SubMenu
              key="subMenuKey"
              icon={<FormOutlined />}
              title={<span>Шаблоны</span>}
            >
              {templates?.map(item =>
              <Menu.Item
                key={item}
                onClick={() => changeTemplate(item)}
              >
                {item}
              </Menu.Item>)}
            </Menu.SubMenu>
          </Menu>
      </Sider>
  );
}