import styles from "../styles/Home.module.css";
import React from "react";
import { useRouter } from 'next/router';
import { Unity, useUnityContext } from "react-unity-webgl";

export default function Home() {
  let sent = false;
  const gameName = "build";
  const {
    unityProvider,
    sendMessage,
    isLoaded,
    loadingProgression
    } = useUnityContext({
      loaderUrl: "build/" + gameName + ".loader.js",
      dataUrl: "build/" + gameName + ".data",
      frameworkUrl: "build/" + gameName + ".framework.js",
      codeUrl: "build/" + gameName + ".wasm",
  });
  
  function setUserName() {
    const router = useRouter();
    let token = router.query["token"];
    if (!token || sent) {
      return;
    }
    sent = true;
    sendMessage("UserNameView(Clone)", "SetUserJWT", token);
  };

  return (
    <div className={styles.container}>
      <div className={styles.unityWrapper}>
        {isLoaded === false && (
          <div className={styles.loadingBar}>
            <div
              className={styles.loadingBarFill}
              style={{ width: loadingProgression * 396 }}
            />
          </div>
        )}
        <Unity
          onload={setUserName()}
          unityProvider={unityProvider}
          style={{ display: isLoaded ? "block" : "none" }}
        />
      </div>
    </div>
  );
}
