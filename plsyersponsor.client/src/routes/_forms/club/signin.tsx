import { Box, Button, Container, Divider, Grid, Paper, TextField, Typography } from '@mui/material';
import { formOptions, useForm } from '@tanstack/react-form';
import { createFileRoute } from '@tanstack/react-router';
import { ClubSignInRequest } from '../../../queries/users/types';
import FieldInfo from '../../../components/forms/FieldInfo';

const defaultFormData: ClubSignInRequest = {
  email: '',
  password: ''
};

const SignIn = () => {
  const formOpts = formOptions({
    defaultValues: defaultFormData
  });

  const form = useForm({
    ...formOpts,
    onSubmit: async ({ value }) => console.log(value)
  });

  const handleSave = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    await form.handleSubmit(e);
  };

  return (
    <Container maxWidth="md" sx={{ py: 4, minHeight: "100vh", backgroundColor: "#78a2cdff" }}>
      <Paper elevation={4} sx={{ borderRadius: '8px' }}>
        <Box sx={{ p: 2, display: 'flex', justifyContent: 'center', alignItems: 'center', bgcolor: 'transparent', borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }}>
          <Box
            component="img"
            src={'/playersponsor-logo.png'}
            alt="PlayerSponsor logo"
            sx={{ height: 80, objectFit: 'contain' }}
          />
        </Box>
        <Divider />

        {/* Header (Simulating AppBar) */}
        <Grid container justifyContent={'center'} sx={{ p: 3, borderTopLeftRadius: '8px', borderTopRightRadius: '8px' }} >
          <Typography variant="h4" component="h1" sx={{ fontWeight: 700, display: "flex", alignItems: "center", gap: 1 }}>
            Sign In as Club Administrator
          </Typography>
        </Grid>

        <Box component="form" onSubmit={handleSave} sx={{ p: { xs: 3, sm: 4 }, display: "flex", flexDirection: "column", gap: 5 }}>

          <form.Field
            name="email"
            children={(field) => (
              <>
                <TextField
                  label="Email"
                  fullWidth
                  margin="normal"
                  variant="outlined"
                  type="email"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(e.target.value)}
                  sx={{ '& .MuiOutlinedInput-root': { borderColor: 'error.main' } }}
                  required
                />
                <FieldInfo field={field} formValidationErrors={{}} />
              </>
            )}
          />

          <form.Field
            name="password"
            children={(field) => (
              <>
                <TextField
                  label="Password"
                  fullWidth
                  margin="normal"
                  variant="outlined"
                  type="password"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(e.target.value)}
                  sx={{ '& .MuiOutlinedInput-root': { borderColor: 'error.main' } }}
                  required
                />
                <FieldInfo field={field} formValidationErrors={{}} />
              </>
            )}
          />

          <Button
            type="submit"
            variant="contained"
            color="primary"
            size="large"
            // disabled={createClubMutation.isPending}
            sx={{ mt: 2, py: 1.5, borderRadius: '24px', fontWeight: 600, fontSize: '1.1rem' }}
          >
            Login {/* {createClubMutation.isPending ? 'Saving...' : 'Next'} */}
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export const Route = createFileRoute('/_forms/club/signin')({
  component: SignIn,
})