import { FC } from "react";
import { Button, Header, Image, Item, Label, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { Link } from "react-router-dom";
import { format } from "date-fns";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

const activityImageStyle = {
  filter: "brightness(30%)",
};

const activityImageTextStyle = {
  position: "absolute",
  bottom: "5%",
  left: "5%",
  width: "100%",
  height: "auto",
  color: "white",
};

interface IActivityDetailHeader {
  activity: Activity;
}

const ActivityDetailHeader: FC<IActivityDetailHeader> = ({ activity }) => {
  const {
    activityStore: { updateAttendance, isLoading, cancelActivityToggle },
  } = useStore();

  return (
    <Segment.Group>
      <Segment basic attached="top" style={{ padding: "0" }}>
        {activity.isCanceled && (
          <Label
            style={{ position: "absolute", zIndex: 1000, left: -14, top: 20 }}
            ribbon
            color="red"
            content="Canceled"
          />
        )}
        <Image
          src={`/assets/categoryImages/${activity.category}.jpg`}
          fluid
          style={activityImageStyle}
        />
        <Segment style={activityImageTextStyle} basic>
          <Item.Group>
            <Item>
              <Item.Content>
                <Header
                  size="huge"
                  content={activity.title}
                  style={{ color: "white" }}
                />
                <p>{format(activity.date!, "dd MMM yyyy")}</p>
                <p>
                  Hosted by{" "}
                  <strong>
                    <Link to={`/profiles/${activity.host?.username}`}>
                      {activity.host?.displayName}
                    </Link>
                  </strong>
                </p>
              </Item.Content>
            </Item>
          </Item.Group>
        </Segment>
      </Segment>
      <Segment clearing attached="bottom">
        {activity.isHost ? (
          <>
            <Button
              color={activity.isCanceled ? "green" : "red"}
              floated="left"
              basic
              content={
                activity.isCanceled ? "Re-activate Activity" : "Cancel Activity"
              }
              onClick={cancelActivityToggle}
              loading={isLoading}
            />
            <Button
              disabled={activity.isCanceled}
              as={Link}
              to={`/manage/${activity.id}`}
              color="orange"
              floated="right"
            >
              Manage Event
            </Button>
          </>
        ) : activity.isGoing ? (
          <Button onClick={updateAttendance} loading={isLoading}>
            Cancel attendance
          </Button>
        ) : (
          <Button
            disabled={activity.isCanceled}
            color="teal"
            onClick={updateAttendance}
            loading={isLoading}
          >
            Join Activity
          </Button>
        )}
      </Segment>
    </Segment.Group>
  );
};

export default observer(ActivityDetailHeader);
