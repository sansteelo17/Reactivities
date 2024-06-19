import { Tab, TabPane } from "semantic-ui-react";
import ProfilePhotos from "./ProfilePhotos";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Profile } from "../../app/models/profile";

interface IProfileContent {
  profile: Profile;
}

const ProfileContent: FC<IProfileContent> = ({ profile }) => {
  const panes = [
    {
      menuItem: "About",
      render: () => <TabPane>About Content</TabPane>,
    },
    {
      menuItem: "Photos",
      render: () => <ProfilePhotos profile={profile}/>,
    },
    {
      menuItem: "Events",
      render: () => <TabPane>Events Content</TabPane>,
    },
    {
      menuItem: "Followers",
      render: () => <TabPane>Followers Content</TabPane>,
    },
    {
      menuItem: "Following",
      render: () => <TabPane>Following Content</TabPane>,
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
    />
  );
};

export default observer(ProfileContent);
