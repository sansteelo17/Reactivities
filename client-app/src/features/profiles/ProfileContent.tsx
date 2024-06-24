import { Tab, TabPane } from "semantic-ui-react";
import ProfilePhotos from "./ProfilePhotos";
import { observer } from "mobx-react-lite";
import { FC } from "react";
import { Profile } from "../../app/models/profile";
import ProfileFollowings from "./ProfileFollowings";
import { useStore } from "../../app/stores/store";
import ProfileActivities from "./ProfileActivities";

interface IProfileContent {
  profile: Profile;
}

const ProfileContent: FC<IProfileContent> = ({ profile }) => {
  const { profileStore } = useStore();
  const panes = [
    {
      menuItem: "About",
      render: () => <TabPane>About Content</TabPane>,
    },
    {
      menuItem: "Photos",
      render: () => <ProfilePhotos profile={profile} />,
    },
    {
      menuItem: "Events",
      render: () => <ProfileActivities />,
    },
    {
      menuItem: "Followers",
      render: () => <ProfileFollowings />,
    },
    {
      menuItem: "Following",
      render: () => <ProfileFollowings />,
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="right"
      panes={panes}
      onTabChange={(_, data) =>
        profileStore.setActiveTab(data.activeIndex as number)
      }
    />
  );
};

export default observer(ProfileContent);
