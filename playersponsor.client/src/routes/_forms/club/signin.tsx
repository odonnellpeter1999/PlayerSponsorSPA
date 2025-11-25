import { Alert, Box, Button, Container, Divider, Grid, Paper, TextField, Typography } from '@mui/material';
import { useForm } from '@tanstack/react-form';
import { createFileRoute, useNavigate } from '@tanstack/react-router';
import { ClubSignInRequest } from '../../../queries/users/types';
import FieldInfo from '../../../components/forms/FieldInfo';
import { useMutation } from '@tanstack/react-query';
import { signInClub } from '../../../queries/users/mutation-functions';
import { WarningAmber } from '@mui/icons-material';

const SignIn = () => {
  const navigate = useNavigate({ from: '/club/dashboard' })

  const signInMutation = useMutation({
    mutationFn: signInClub,
    onSuccess: () => {
      navigate({ to: '/club/dashboard' });
    }
  });

  const form = useForm({
    onSubmit: async ({ value }) => onSubmit(value as ClubSignInRequest),
  });

  const onSubmit = async (request: ClubSignInRequest) => {
    await signInMutation.mutateAsync(request);
  };

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
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

        <Box component="form" onSubmit={handleLogin} sx={{ p: { xs: 3, sm: 4 }, display: "flex", flexDirection: "column", gap: 5 }}>
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

          {/* Global Error Message */}
          {signInMutation.isError && (
            <Alert icon={<WarningAmber fontSize="inherit" />} severity="error" sx={{ borderRadius: 0 }}>
              {'An error occurred while signing in. Please check your credentials and try again.'}
            </Alert>
          )}

          <Button
            type="submit"
            variant="contained"
            color="primary"
            size="large"
            disabled={signInMutation.isPending}
            sx={{ mt: 2, py: 1.5, borderRadius: '24px', fontWeight: 600, fontSize: '1.1rem' }}
          >
            {signInMutation.isPending ? 'Loading...' : 'Login'}
          </Button>
        </Box>
      </Paper>
    </Container>
  );
};

export const Route = createFileRoute('/_forms/club/signin')({
  component: SignIn,
})