import React from 'react';
import { ThemeProvider as MuithemeProvider} from "@material-ui/core/styles";
import theme from "./theme/theme";
import RegistrarUsuario from './components/Seguridad/RegistrarUsuario';

function App() {
  return (
    <MuithemeProvider theme={theme}>
      <RegistrarUsuario/>
    </MuithemeProvider>
  );
}

export default App;
