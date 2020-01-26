import React from 'react';
import { Modal, ModalBody, ModalFooter, Button } from 'reactstrap';

export default function Question(props) {
    const { showModal, content, yesClicked, noClicked } = props;

    return (
        <Modal isOpen={showModal}>
            <ModalBody>
                {content}
            </ModalBody>
            <ModalFooter>
                <Button color={'secondary'} onClick={noClicked}>No</Button>
                <Button
                    color={'success'}
                    onClick={yesClicked}>
                    Yes
                </Button>
            </ModalFooter>
        </Modal>
    )
}