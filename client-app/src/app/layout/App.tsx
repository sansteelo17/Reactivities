import { Container } from "semantic-ui-react";
import Navbar from "./Navbar";
import { observer } from "mobx-react-lite";
import { Outlet, useLocation } from "react-router-dom";
import Homepage from "../../features/home/Homepage";
import { ToastContainer } from "react-toastify";

function App() {
  const loacation = useLocation();

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      {loacation.pathname === "/" ? (
        <Homepage />
      ) : (
        <>
          <Navbar />
          <Container style={{ marginTop: "7em" }}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

export default observer(App);
