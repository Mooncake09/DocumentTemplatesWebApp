import React, { FC } from "react";
import { Layout, Menu } from 'antd';
import type { MenuProps } from "antd";
import { FormOutlined } from '@ant-design/icons';
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
): MenuItem {
  return {
    key,
    icon,
    children,
    label,
    type,
  } as MenuItem;
}

export const SiderMenu: FC<Props> = (props: Props) => {

  const { templates } = props;

  // const sidebarItems: MenuProps['items'] = [FormOutlined].map(
  //   (icon, index) => {
  //     const key = String(index + 1);
  
  //     return {
  //       key: `sub${key}`,
  //       icon: React.createElement(icon),
  //       label: `Шаблоны`,
  
  //       children: new Array(4).fill(null).map((_, j) => {
  //         const subKey = index * 4 + j + 1;
  //         return {
  //           key: subKey,
  //           label: `option${subKey}`,
  //         };
  //       }),
  //     };
  //   },
  // );

  const sidebarItems: MenuItem[] = [
    getItem('Шаблоны', 'sub1', <FormOutlined />, 
      templates.map((item, index) => getItem(item, index))
    )
  ];

  return(
      <Sider className="site-layout-background" width={200}>
          <Menu
              mode="inline"
              defaultSelectedKeys={['1']}
              defaultOpenKeys={['sub1']}
              style={{ height: '100%' }}
              items={sidebarItems}
          />
      </Sider>
  );
}