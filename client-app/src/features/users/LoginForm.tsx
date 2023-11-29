import { Formik, Form, ErrorMessage } from "formik"
import MyTextInput from "../../app/api/common/form/MyTextInput"
import { Button, Header, Label } from "semantic-ui-react"
import { useStore } from "../../app/stores/store"
import { observer } from "mobx-react-lite"
import { ReactNode } from "react"

const LoginForm = () => {
    const {userStore} = useStore();
  return (
    <Formik
        initialValues={{email: '', password: '', error: null}}
        onSubmit={(values, {setErrors}) => userStore.login(values).catch(() => 
            setErrors({error: 'Invalid email or Password'}))}
    >
        {({handleSubmit, isSubmitting, errors}) => (
            <Form className="ui form" onSubmit={handleSubmit} autoComplete='off'>
                <Header as='h2' content='Login to Reactivities' color="teal" textAlign="center" />
                <MyTextInput placeholder="Email" name='email'/>
                <MyTextInput placeholder="Password" name='password' type='password'/>
                <ErrorMessage 
                    name='error' 
                    render={() => <Label style={{marginBottom: 10}} basic content={errors.error as ReactNode} color="red" />}
                /> 
                <Button loading={isSubmitting} positive content='Login' type="submit" fluid />
            </Form>
        )}      
    </Formik>
  )
}
export default observer(LoginForm);