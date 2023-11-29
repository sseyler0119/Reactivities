import { Segment, Button, Header } from 'semantic-ui-react';
import { ActivityFormValues } from '../../../app/models/activity';
import { useEffect, useState } from 'react';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { Link, useNavigate, useParams } from 'react-router-dom';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import {v4 as uuid} from 'uuid';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import MyTextInput from '../../../app/api/common/form/MyTextInput';
import MyTextArea from '../../../app/api/common/form/MyTextArea';
import MySelectInput from '../../../app/api/common/form/MySelectInput';
import { categoryOptions } from '../../../app/api/common/options/categoryOptions';
import MyDateInput from '../../../app/api/common/form/MyDateInput';


const ActivityForm = () => {
  const {activityStore} = useStore();
  const {createActivity, updateActivity, loadActivity, loadingInitial} = activityStore;
  const {id} = useParams();
  const navigate = useNavigate();

  const [activity, setActivity] = useState<ActivityFormValues>(new ActivityFormValues());

  const validationSchema = Yup.object({
    title: Yup.string().required('The activity title is required'),    
    description: Yup.string().required('The activity description is required'),    
    category: Yup.string().required(),    
    date: Yup.string().required('Date is required'),    
    venue: Yup.string().required(),    
    city: Yup.string().required(),    

  })
  useEffect(() => {
    if(id) loadActivity(id).then(activity => setActivity(new ActivityFormValues(activity)));
  }, [id, loadActivity]);
  

  const handleFormSubmit = (activity: ActivityFormValues) => {
    if(!activity.id) {
      let newActivity = {
        ...activity,
        id: uuid(),
      }
      createActivity(newActivity).then(() => navigate(`/activities/${newActivity.id}`));
    } else {
      updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
    }
  };

  
  if(loadingInitial) return <LoadingComponent content='Loading activity...' />
  
  return (
    <Segment clearing>
      <Header content='Activity Details' sub color='teal' />
      <Formik
        validationSchema={validationSchema}
        enableReinitialize
        initialValues={activity}
        onSubmit={(values) => handleFormSubmit(values)}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty }) => (
          <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
            <MyTextInput name='title' placeholder='Title' />
            <MyTextArea placeholder='Description' name='description' rows={3} />
            <MySelectInput
              options={categoryOptions}
              placeholder='Category'
              name='category'
            />
            <Header content='Location Details' sub color='teal' />

            <MyDateInput
              placeholderText='Date'
              name='date'
              showTimeSelect
              timeCaption='time'
              dateFormat='MMMM d, yyyy, h:mm aa'
            />
            <MyTextInput placeholder='City' name='city' />
            <MyTextInput placeholder='Venue' name='venue' />
            <Button
              disabled={isSubmitting || !dirty || !isValid}
              loading={isSubmitting}
              floated='right'
              positive
              type='submit'
              content='Submit'
            />
            <Button
              as={Link}
              to='/activities'
              floated='right'
              type='button'
              content='Cancel'
            />
          </Form>
        )}
      </Formik>
    </Segment>
  );
};
export default observer(ActivityForm);
