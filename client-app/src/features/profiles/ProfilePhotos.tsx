import { observer } from "mobx-react-lite";
import { FC, SyntheticEvent, useState } from "react";
import { Button, Card, Grid, Header, Image, TabPane } from "semantic-ui-react";
import { Photo, Profile } from "../../app/models/profile";
import { useStore } from "../../app/stores/store";
import PhotoUploadWidget from "../../app/common/image-upload/PhotoUploadWidget";

interface IProfilePhotos {
  profile: Profile;
}

const ProfilePhotos: FC<IProfilePhotos> = ({ profile }) => {
  const {
    profileStore: {
      isCurrentUser,
      uploadPhoto,
      uploading,
      loading,
      setMainPhoto,
      deletePhoto,
    },
  } = useStore();
  const [addPhotoMode, setAddPhotoMode] = useState(false);
  const [target, setTarget] = useState("");

  const handlePhotoUpload = (file: Blob) => {
    uploadPhoto(file).then(() => setAddPhotoMode(false));
  };

  const handleSetMainPhoto = (
    photo: Photo,
    e: SyntheticEvent<HTMLButtonElement>
  ) => {
    setTarget(e.currentTarget.name);
    setMainPhoto(photo);
  };

  const handleDeletePhoto = (
    photo: Photo,
    e: SyntheticEvent<HTMLButtonElement>
  ) => {
    setTarget(e.currentTarget.name);
    deletePhoto(photo);
  };

  return (
    <TabPane>
      <Grid>
        <Grid.Column width={16}>
          <Header floated="left" icon="image" content="Photos" />
          {isCurrentUser && (
            <Button
              floated="right"
              basic
              content={addPhotoMode ? "Cancel" : "Add Photo"}
              onClick={() => setAddPhotoMode(!addPhotoMode)}
            />
          )}
        </Grid.Column>
        <Grid.Column width={16}>
          {addPhotoMode ? (
            <PhotoUploadWidget
              uploadPhoto={handlePhotoUpload}
              isLoading={uploading}
            />
          ) : (
            <Card.Group itemsPerRow={5}>
              {profile.photos?.map((photo) => (
                <Card key={photo.id}>
                  <Image src={photo.url} />
                  {isCurrentUser && (
                    <Button.Group fluid widths={2}>
                      <Button
                        basic
                        color="green"
                        content="Main"
                        name={"main" + photo.id}
                        disabled={photo.isMain}
                        loading={target === "main" + photo.id && loading}
                        onClick={(e) => handleSetMainPhoto(photo, e)}
                      />
                      <Button
                        basic
                        color="red"
                        icon="trash"
                        loading={target === photo.id && loading}
                        onClick={(e) => handleDeletePhoto(photo, e)}
                        disabled={photo.isMain}
                        name={photo.id}
                      />
                    </Button.Group>
                  )}
                </Card>
              ))}
            </Card.Group>
          )}
        </Grid.Column>
      </Grid>
    </TabPane>
  );
};

export default observer(ProfilePhotos);
